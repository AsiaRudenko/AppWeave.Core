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

using System.Collections.Generic;
using System.Diagnostics;

using AppWeave.Core.Utils;

using JetBrains.Annotations;

namespace AppWeave.Core.Processes
{
    /// <summary>
    /// Represents the arguments for a process as a list (rather than a single string). Provides
    /// convenience methods to create the arguments list as well as apply it to an instance
    /// of <see cref="ProcessStartInfo"/>.
    /// </summary>
    [PublicAPI]
    public class ProcessArguments : List<string>
    {
        /// <summary>
        /// Creates an instance of this class and populates it with the specified arguments.
        /// </summary>
        public ProcessArguments([NotNull] params string[] args)
            : base(args)
        {
        }

        /// <summary>
        /// Creates an instance of this class and populates it with a copy of the specified collection.
        /// </summary>
        public ProcessArguments([NotNull] IEnumerable<string> collection) : base(collection)
        {
        }

        /// <summary>
        /// Copies the arguments in this instance to the specified instance of <see cref="ProcessStartInfo"/>.
        /// </summary>
        public void CopyTo([NotNull] ProcessStartInfo processStartInfo)
        {
            Verify.ParamNotNull(processStartInfo, nameof(processStartInfo));

            // Must be set to empty when using "ArgumentsList".
            processStartInfo.Arguments = "";

            var startInfoArguments = processStartInfo.ArgumentList;

            // Make sure it's empty.
            startInfoArguments.Clear();

            foreach (var processArgument in this)
            {
                if (processArgument != null)
                {
                    startInfoArguments.Add(processArgument);
                }
            }
        }
    }
}
