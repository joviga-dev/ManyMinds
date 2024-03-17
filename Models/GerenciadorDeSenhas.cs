﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ManyMindsApi.Models;
[NotMapped]
public class GerenciadorDeSenhas
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

}
