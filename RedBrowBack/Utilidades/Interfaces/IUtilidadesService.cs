﻿namespace RedBrowBack.Utilidades.Interfaces
{
    public interface IUtilidadesService
    {
        string GenerarClave();

        string ConvertirSha256(string? texto);
    }
}
