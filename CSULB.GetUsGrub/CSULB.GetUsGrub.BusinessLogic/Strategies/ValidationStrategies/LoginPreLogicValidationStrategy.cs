﻿using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class LoginPreLogicValidationStrategy
    {
        private readonly LoginDto _loginDto;
        private readonly LoginDtoValidator _loginDtoValidator = new LoginDtoValidator();

        public LoginPreLogicValidationStrategy(LoginDto loginDto)
        {
            _loginDto = loginDto;
        }

        public ResponseDto<LoginDto> ExecuteStrategy()
        {
            // TODO @Ahmed Put the Wrapper here @Ahmed
            var validationWrapper =
                new ValidationWrapper<LoginDto>(_loginDto, "UsernameAndPassword", new LoginDtoValidator());
            var validationResult = validationWrapper.ExecuteValidator();

            // Checking if there is an error in the validation
            if (!validationResult.Data)
            {
                // Return an error if validation fails
                return new ResponseDto<LoginDto>()
               {
                   Error = GeneralErrorMessages.GENERAL_ERROR
               };
            }

            // Checking if user Exists
            var userExistanceValidator = new UserValidator();
            var validateUserExistanceResult = userExistanceValidator.CheckIfUserExists(_loginDto.Username);
            if (!validateUserExistanceResult.Data)
            {
                return new ResponseDto<LoginDto>
                {
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }
            
            // Returning the Dto Back if after it has been validated
            return new ResponseDto<LoginDto>
            {
                Data = _loginDto
            };
        }
    }
}
