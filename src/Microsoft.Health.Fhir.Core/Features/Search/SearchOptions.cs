﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Health.Fhir.Core.Features.Search.Expressions;
using Microsoft.Health.Fhir.Core.Models;

namespace Microsoft.Health.Fhir.Core.Features.Search
{
    /// <summary>
    /// Represents the search options.
    /// </summary>
    public class SearchOptions
    {
        private int _maxItemCount;
        private int _includeCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchOptions"/> class.
        /// It hides constructor and prevent object creation not through <see cref="ISearchOptionsFactory"/>
        /// </summary>
        internal SearchOptions()
        {
        }

        /// <summary>
        /// Gets the optional continuation token.
        /// </summary>
        public string ContinuationToken { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether to only return the record count
        /// </summary>
        public bool CountOnly { get; internal set; }

        /// <summary>
        /// Indicates if the total number of resources that match the search parameters should be calculated.
        /// </summary>
        /// <remarks>The ability to retrieve an estimate of the total is yet to be implemented.</remarks>
        public TotalType IncludeTotal { get; internal set; }

        /// <summary>
        /// Gets the maximum number of items to find.
        /// </summary>
        public int MaxItemCount
        {
            get => _maxItemCount;

            internal set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException(Resources.InvalidSearchCountSpecified);
                }

                _maxItemCount = value;
            }
        }

        /// <summary>
        /// Get the number of items to include in search results.
        /// </summary>
        public int IncludeCount
        {
            get => _includeCount;
            internal set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException(Resources.InvalidSearchCountSpecified);
                }

                _includeCount = value;
            }
        }

        /// <summary>
        /// Gets the search expression.
        /// </summary>
        public Expression Expression { get; internal set; }

        /// <summary>
        /// Gets the list of search parameters that were not used in the search.
        /// </summary>
        public IReadOnlyList<Tuple<string, string>> UnsupportedSearchParams { get; internal set; }

        /// <summary>
        /// Gets the list of sorting parameters.
        /// </summary>
        public IReadOnlyList<(SearchParameterInfo searchParameterInfo, SortOrder sortOrder)> Sort { get; internal set; }
    }
}
