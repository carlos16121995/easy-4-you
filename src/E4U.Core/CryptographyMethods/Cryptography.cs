// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

using System.Text;

using Blake3;

namespace E4U.Core.CryptographyMethods;

/// <summary>
/// Classe para gerar Hashs
/// </summary>
public static class Cryptography
{
    /// <summary>
    /// Gera um hash para um arquivo
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>The calculated 256-bit/32-byte hash.</returns>
    public static string GenerateFileHashBlake3(string filePath)
    {
        using (var stream = File.OpenRead(filePath))
        {
            using var blake3Stream = new Blake3Stream(stream);
            blake3Stream.Write(Encoding.UTF8.GetBytes("BLAKE3"));
            var hash = blake3Stream.ComputeHash();
            return hash.ToString();
        }
    }

    /// <summary>
    /// Gera um hash para uma string
    /// </summary>
    /// <param name="input"></param>
    /// <returns>The calculated 256-bit/32-byte hash.</returns>
    public static string GenerateBlake3HashFromString(string input)
    {
        // Convertendo a string para um array de bytes
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // Gerando o hash Blake3
        var hash = Blake3.Hasher.Hash(inputBytes);

        // Convertendo o hash para uma string hexadecimal
        return hash.ToString();
    }
}
