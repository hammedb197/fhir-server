﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using Hl7.Fhir.Model;
using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;
using Microsoft.Health.Fhir.SqlServer.Features.Schema.Model;

namespace Microsoft.Health.Fhir.SqlServer.Features.Storage.TvpRowGeneration
{
    internal class NumberSearchParameterRowGenerator : SearchParameterRowGenerator<NumberSearchValue, V1.NumberSearchParamTableTypeRow>
    {
        public NumberSearchParameterRowGenerator(SqlServerFhirModel model)
            : base(model)
        {
        }

        protected override V1.NumberSearchParamTableTypeRow GenerateRow(short searchParamId, SearchParameter searchParameter, NumberSearchValue searchValue)
        {
            bool isSingleValue = searchValue.Low == searchValue.High;

            return new V1.NumberSearchParamTableTypeRow(
                searchParamId,
                isSingleValue ? searchValue.Low : null,
                isSingleValue ? null : searchValue.Low ?? (decimal?)V1.NumberSearchParam.LowValue.MinValue,
                isSingleValue ? null : searchValue.High ?? (decimal?)V1.NumberSearchParam.HighValue.MaxValue);
        }
    }
}