﻿using NsisoLauncherCore.Modules;
using NsisoLauncherCore.Net.MojangApi.Api;
using System;
using System.Threading.Tasks;
using static NsisoLauncherCore.Net.MojangApi.Responses.AuthenticateResponse;

namespace NsisoLauncherCore.Auth
{
    public class OfflineAuthenticator : IAuthenticator
    {
        public string Displayname { get; set; }
        public UserData UserData { get; set; }
        public PlayerProfile ProfileUUID { get; set; }

        public OfflineAuthenticator(string displayname)
        {
            this.Displayname = displayname;
            this.ProfileUUID = new PlayerProfile()
            {
                PlayerName = Displayname,
                Value = Guid.NewGuid().ToString("N")
            };
            this.UserData = new UserData()
            {
                ID = Guid.NewGuid().ToString("N")
            };
        }

        public OfflineAuthenticator(string displayname, UserData userData, string profileUUID)
        {
            this.Displayname = displayname;
            this.UserData = userData;
            this.ProfileUUID = new PlayerProfile()
            {
                PlayerName = Displayname,
                Value = profileUUID
            };
        }

        public AuthenticateResult DoAuthenticate()
        {
            string accessToken = Guid.NewGuid().ToString("N");

            return new AuthenticateResult(AuthState.SUCCESS) { AccessToken = accessToken, SelectedProfile = this.ProfileUUID, UserData = this.UserData };
        }

        public async Task<AuthenticateResult> DoAuthenticateAsync()
        {
            return await Task.Factory.StartNew(() => { return DoAuthenticate(); });
        }
    }
}
