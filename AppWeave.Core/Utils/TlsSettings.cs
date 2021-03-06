﻿#region License
// Copyright 2020 - 2020 AppWeave.Core (https://github.com/skrysmanski/AppWeave.Core)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Net;

using JetBrains.Annotations;

namespace AppWeave.Core.Utils
{
    /// <summary>
    /// Manages SSL/TLS settings and protocols.
    /// </summary>
    public static class TlsSettings
    {
        // NOTE: This value is copied from "SecurityProtocolType.Tls13" which is not available in .NET Standard 2.1.
        private const SecurityProtocolType TLS_13 = (SecurityProtocolType)0x00003000;

        /// <summary>
        /// Enables and disables SSL and TLS protocols for the currently running process - based on current best practices.
        /// </summary>
        [PublicAPI]
        public static void ApplyToCurrentProcess()
        {
            // NOTE: The default value of "ServicePointManager.SecurityProtocol" depends on the .NET Framework
            //   being used. This is why this method makes sure the value is the same across all frameworks.

            EnableProtocol(TLS_13); // TLS 1.3
            EnableProtocol(SecurityProtocolType.Tls12); // TLS 1.2

            // NOTE: TLS 1.0 and 1.1 are considered obsolete/outdated nowadays and are disabled on more and more
            //   server. To avoid downgrade attacks, we'll disable them for clients as well.
            DisableProtocol(SecurityProtocolType.Tls); // TLS 1.0
            DisableProtocol(SecurityProtocolType.Tls11); // TLS 1.1

#pragma warning disable 618
            // SSL 3 is generally considered insecure and should never be used anymore.
            DisableProtocol(SecurityProtocolType.Ssl3);
#pragma warning restore 618
        }

        /// <summary>
        /// Globally enables the specified protocol.
        /// </summary>
        [PublicAPI]
        public static void EnableProtocol(SecurityProtocolType protocol)
        {
            ServicePointManager.SecurityProtocol |= protocol;
        }

        /// <summary>
        /// Globally enables the specified protocol.
        /// </summary>
        [PublicAPI]
        public static void DisableProtocol(SecurityProtocolType protocol)
        {
            ServicePointManager.SecurityProtocol &= ~protocol;
        }
    }
}
