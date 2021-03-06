﻿using CSULB.GetUsGrub.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service for accessing Google's Timezone API to calculate a location's offset from UTC.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public class GoogleTimeZoneService : ITimeZoneService, ITimeZoneServiceAsync
    {
        private string BuildUrl(IGeoCoordinates coordinates, string key, int timestamp)
        {
            var url = GoogleApiConstants.GOOGLE_TIMEZONE_URL;
            url += $"{GoogleApiConstants.GOOGLE_TIMEZONE_LOCATION_QUERY}{coordinates.Latitude},{coordinates.Longitude}";
            url += GoogleApiConstants.GOOGLE_TIMEZONE_TIMESTAMP_QUERY + timestamp;
            url += GoogleApiConstants.GOOGLE_KEY_QUERY + key;

            return url;
        }

        /// <summary>
        /// Calculates offset from UTC from geocoordinates using Google's Timezone API.
        /// This is a synchronous method wrapped around GetOffsetAsync()
        /// </summary>
        /// <param name="coordinates">Coordinates of location to check time zone.</param>
        /// <returns>Offset of time (in seconds) from UTC</returns>
        public ResponseDto<int> GetOffset(IGeoCoordinates coordinates)
        {
            var result = Task.Run(() => GetOffsetAsync(coordinates)).Result;

            return result;
        }

        /// <summary>
        /// Calculates offset from UTC from geocoordinates using Google's Timezone API.
        /// </summary>
        /// <param name="coordinates">Coordinates of location to check time zone.</param>
        /// <returns>Offset of time (in seconds) from UTC.</returns>
        public async Task<ResponseDto<int>> GetOffsetAsync(IGeoCoordinates coordinates)
        {
            // Get current date in seconds to determine if its DST for Google's API.
            var baseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            var timeStamp = (int)DateTime.Now.Subtract(baseTime).TotalSeconds;

            // Retrieve key from configuration and build url for get request.
            var key = ConfigurationManager.AppSettings[GoogleApiConstants.GOOGLE_TIMEZONE_API_KEYWORD];
            var url = BuildUrl(coordinates, key, timeStamp);

            // Send get request and parse response
            var request = new GetRequestService(url);
            var response = await new GoogleBackoffRequest(request).TryExecute();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<GoogleTimeZoneDto>(responseJson);
            
            if (responseObj.status != GoogleApiConstants.GOOGLE_GEOCODE_STATUS_OK)
            {
                if (responseObj.status == GoogleApiConstants.GOOGLE_TIMEZONE_STATUS_ZERO_RESULTS)
                {
                    return new ResponseDto<int>()
                    {
                        Error = GoogleApiConstants.GOOGLE_TIMEZONE_ERROR_INVALID_ADDRESS
                    };
                }

                return new ResponseDto<int>()
                {
                    Error = GoogleApiConstants.GOOGLE_TIMEZONE_ERROR_GENERAL
                };
            }

            return new ResponseDto<int>()
            {
                Data = responseObj.rawOffset + responseObj.dstOffset
            };
        }
    }
}