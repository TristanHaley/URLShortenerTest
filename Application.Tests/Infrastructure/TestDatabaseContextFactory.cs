// Created by: Haley, Tristan (th185132) on: 17/06/2019 at: 13:35.
// Project: Mercury\Mercury.Application.Tests
// Copyright: © 2019 NCR. All Rights Reserved.
// Filename: TestMercuryDatabaseContextFactory.cs

using System;
using System.Data.Common;
using Application.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tests.Infrastructure
{
    public static class TestDatabaseContextFactory
    {
        public static UrlShortenerContext Create()
        {
            var options = new DbContextOptionsBuilder<UrlShortenerContext>()
                         .UseSqlite(CreateInMemoryDatabase())
                         .Options;

            var context = new UrlShortenerContext(options);
            context.Database.EnsureCreated();
            context.SaveChanges();

            return context;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            
            connection.Open();
            
            return connection;
        }

        public static void Destroy(UrlShortenerContext mercuryContext)
        {
            mercuryContext.Database.EnsureDeleted();
            mercuryContext.Dispose();
        }
    }
}