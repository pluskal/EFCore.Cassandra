﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.EntityFrameworkCore.Cassandra.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Microsoft.EntityFrameworkCore
{
    public static class CassandraDbContextOptionsExtensions
    {
        public static DbContextOptionsBuilder UseCassandra(this DbContextOptionsBuilder  optionsBuilder, string connectionString, Action<CassandraDbContextOptionsBuilder> cassandraOptionsAction = null)
        {
            var extension = (CassandraOptionsExtension)GetOrCreateExtension(optionsBuilder).WithConnectionString(connectionString);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
            cassandraOptionsAction?.Invoke(new CassandraDbContextOptionsBuilder(optionsBuilder));
            return optionsBuilder;
        }

        private static CassandraOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder options) => options.Options.FindExtension<CassandraOptionsExtension>() ?? new CassandraOptionsExtension();
    }
}
