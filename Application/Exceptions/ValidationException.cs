// Created by: Haley, Tristan (th185132) on: 13/06/2019 at: 13:57.
// Project: Mercury\Mercury.Application
// Copyright: © 2020 NCR. All Rights Reserved.
// Filename: ValidationException.cs

using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        #region Constructors

        public ValidationException() : base("One or more validation failures have occurred")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                               .Select(e => e.PropertyName)
                               .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                                      .Where(e => e.PropertyName == propertyName)
                                      .Select(e => e.ErrorMessage)
                                      .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        #endregion

        public IDictionary<string, string[]> Failures { get; }
    }
}