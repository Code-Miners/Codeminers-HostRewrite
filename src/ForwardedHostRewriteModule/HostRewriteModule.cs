//-----------------------------------------------------------------------
// <copyright file="HostRewriteModule.cs" company="Code Miners Limited">
//  Copyright (c) 2019 Code Miners Limited
//   
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//  
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Modules.Web.HttpModules
{
    using System;
    using System.Web;

    /// <summary>
    /// Simple http module for rewriting the forwarded host headers. For when hosted sites can only listen to the host header
    /// and we need to use a WAF or some form of proxy.
    /// </summary>
    public class HostRewriteModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += RewriteHost;
        }

        private void RewriteHost(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;
            if (context == null)
            {
                return;
            }

            // Grab the standard application gateway forwarded host
            string potentialHost = context.Request.Headers["X-Original-Host"];

            if (string.IsNullOrWhiteSpace(potentialHost))
            {
                // Not found, grab the standard forwarded host
                potentialHost = context.Request.Headers["X-Forwarded-Host"];
            }

            if (string.IsNullOrWhiteSpace(potentialHost))
            {
                // No value - nothing to do
                return;
            }

            context.Request.Headers["Host"] = potentialHost;

        }

        public void Dispose()
        {

        }
    }
}
