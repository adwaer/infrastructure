﻿namespace In.Auth.Config
{
    public class AuthenticationSettings
    {
        public string Url { get; set; }
        public string SecretJwtKey { get; set; }
        public int LifeTime { get; set; }
    }
}