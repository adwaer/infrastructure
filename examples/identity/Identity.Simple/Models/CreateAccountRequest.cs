﻿namespace Identity.Simple.Models
{
    /// <summary>
    /// Query model 'Create account'.
    /// </summary>
    public sealed class CreateAccountRequest
    {
        /// <summary>
        /// Pwd
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Confirm owd
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
    }
}