using System;

namespace IPCUtilities
{
    static class Guard
    {
        internal static void ThrowIsNull(object param)
        {
            if (param==null)
                throw new ArgumentNullException();
        }
        internal static void ThrowIsNull(string ceroParam)
        {
            if (string.IsNullOrEmpty(ceroParam))
                throw new ArgumentNullException(ceroParam, "parameters is null");
        }
        internal static void ThrowIsNull(string ceroParam,string primeraParam)
        {
            if (string.IsNullOrEmpty(ceroParam))
                throw new ArgumentNullException(ceroParam, "parameters is null");
            if (string.IsNullOrEmpty(primeraParam))
                throw new ArgumentNullException(primeraParam,"primeraParam is null");
        }
        internal static void ThrowIsNull(string ceroParam, string primeraParam,string segundoParam)
        {
            if (string.IsNullOrEmpty(ceroParam))
                throw new ArgumentNullException(ceroParam,"parameters is null");
            if (string.IsNullOrEmpty(primeraParam))
                throw new ArgumentNullException(primeraParam, "parameters is null");
            if (string.IsNullOrEmpty(segundoParam))
                throw new ArgumentNullException(segundoParam, "parameters is null");
        }
        internal static void ThrowIsNull(string ceroParam, string primeraParam, string segundoParam,string terceraParam)
        {
            if (string.IsNullOrEmpty(ceroParam))
                throw new ArgumentNullException(ceroParam,"parameters is null");
            if (string.IsNullOrEmpty(primeraParam))
                throw new ArgumentNullException(primeraParam, "parameters is null");
            if (string.IsNullOrEmpty(segundoParam))
                throw new ArgumentNullException(segundoParam,"parameters is null");
            if (string.IsNullOrEmpty(terceraParam))
                throw new ArgumentNullException(terceraParam,"parameters is null");
        }
       
    }
}
