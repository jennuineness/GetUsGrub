﻿using CSULB.GetUsGrub.Models;
using System.Data.Entity;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// The SsoContext.
    /// Context containing all tables for SSO management.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class SsoContext : DbContext
    {
        public DbSet<InvalidSsoToken> InvalidSsoTokens { get; set; }
        public DbSet<ValidSsoToken> ValidSsoTokens { get; set; }
        public SsoContext() : base("GetUsGrub") { }
    }
}
