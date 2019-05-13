﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;



    public class SecurityUtils
    {
        public static string EncriptarSHA2(string texto)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(texto);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder hash = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                hash.Append(hashedBytes[i].ToString("x2").ToLower());

            return hash.ToString();
        }
    }
