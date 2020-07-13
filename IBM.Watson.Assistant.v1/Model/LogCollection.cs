/**
* (C) Copyright IBM Corp. 2018, 2019.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// LogCollection.
    /// </summary>
    public class LogCollection
    {
        /// <summary>
        /// An array of objects describing log events.
        /// </summary>
        [JsonProperty("logs", NullValueHandling = NullValueHandling.Ignore)]
        public List<Log> Logs { get; set; }
        /// <summary>
        /// The pagination data for the returned objects.
        /// </summary>
        [JsonProperty("pagination", NullValueHandling = NullValueHandling.Ignore)]
        public LogPagination Pagination { get; set; }
    }

}
