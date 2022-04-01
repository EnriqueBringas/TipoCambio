﻿using System;
using TipoCambio.Api.Auth.Entity.Entities.Models;

namespace TipoCambio.Api.Auth.Service.Services
{
    public abstract class BaseService
    {
        protected ServiceConfig _config;

        protected void ValidRequest<T>(T request)
        {
            if (request == null)
                throw new ArgumentException("No existe ningún parámetro de entrada", "Parameters");
        }
    }
}