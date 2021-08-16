// Created by: Haley, Tristan (th185132) on: 13/06/2019 at: 13:57.
// Project: Mercury\Mercury.Application
// Copyright: © 2020 NCR. All Rights Reserved.
// Filename: DeleteFailureException.cs

using System;

namespace Application.Exceptions
{
    public class DeleteFailureException : Exception
    {
        #region Constructors

        public DeleteFailureException(string name, object key, string message) : base($"Deletion of entity \"{name}\" ({key}) failed") { }

        #endregion
    }
}