using BCrypt.Net;

namespace BlogDaTurma.Services
{
    public class SenhaService
    {
        public string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool Verificar(string senha, string hashArmazenado)
        {
            if (string.IsNullOrEmpty(hashArmazenado)) return false;
            return BCrypt.Net.BCrypt.Verify(senha, hashArmazenado);
        }
    }
}