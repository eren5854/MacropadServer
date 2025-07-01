﻿namespace MacropadServer.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterAuthRoutes();
        app.RegisterMacropadModelRoutes();
        app.RegisterMacropadDeviceRoutes();
    }
}
