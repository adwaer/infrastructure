﻿namespace Identity.Simple.Models
{
    /// <summary>
    /// Query model 'Auth'.
    /// </summary>
    public sealed class PwdAuthRequest
    {
        /// <summary>
        /// Pwd
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
    }
}