﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using MediatR;
using Microsoft.Health.Fhir.SqlServer.Features.Schema;
using Microsoft.Health.Fhir.SqlServer.Features.Schema.Messages.Get;

namespace Microsoft.Health.Fhir.SqlServer.Api.Features
{
    public class LatestSchemaVersionHandler : IRequestHandler<GetCompatibilityVersionRequest, GetCompatibilityVersionResponse>
    {
        private readonly ISchemaMigrationDataStore _schemaMigrationDataStore;

        public LatestSchemaVersionHandler(ISchemaMigrationDataStore schemaMigrationDataStore)
        {
            EnsureArg.IsNotNull(schemaMigrationDataStore, nameof(schemaMigrationDataStore));
            _schemaMigrationDataStore = schemaMigrationDataStore;
        }

        public async Task<GetCompatibilityVersionResponse> Handle(GetCompatibilityVersionRequest request, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(request, nameof(request));

            int maxCompatibleVersion = await _schemaMigrationDataStore.GetLatestCompatibleVersionAsync(cancellationToken);

            return new GetCompatibilityVersionResponse(request.MinVersion, maxCompatibleVersion);
        }
    }
}