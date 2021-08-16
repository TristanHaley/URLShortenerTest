// Created by: Haley, Tristan (th185132) on: 13/06/2019 at: 13:57.
// Project: Mercury\Mercury.Application
// Copyright: © 2020 NCR. All Rights Reserved.
// Filename: NotFoundException.cs

using System;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        #region Constructors

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found") { }

        #endregion
    }
}