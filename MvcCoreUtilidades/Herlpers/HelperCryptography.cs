using System.Security.Cryptography;
using System.Text;

namespace MvcCoreUtilidades.Herlpers
{
    public class HelperCryptography
    {
        public static string Salt { get; set; }

        //VADA VEZ QUE REALCEMOS UN CIFRADO GENERAMOS UN SALT DISTINTO
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for(int i = 1; i<=30; i++)
            {
                //GENERAMOS UN NUMERO ALEATORIO CON CODIGOS ASCII
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static string CifrarContenido(string contenido, bool comparar)
        {
            if(comparar == false)
            {
                //CREAMOS UN NUEVO SALT
                Salt = GenerateSalt();
            }
            //EL SALT LO PODEMOS USAR EN MULTIPLES LUGARES
            string contenidoSalt = contenido + Salt;
            //CREAMOS UN OBJETO GRANDE PARA CIFRAR
            SHA256 managed = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidoSalt);
            //CIFRAMOD EL CONTENIDO N ITERACIONES
            for (int i = 1; i<=22; i++)
            {
                //REALIZAMOS EL CIFRADO SOBRE EL CIFRADO
                salida = managed.ComputeHash(salida);
            }
            //DEBEMOS LIBERAR LA MEMORIA
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        //COMENZAMOS CREANDO UN METODO STATIC
        public static string EncriptarTextoBasico(string contenido)
        {
            //NECESITAMOS UN ARRY DE BYTES PARA CONVERTIR EL CONTENIDO DE ENTRADA A BYTE
            byte[] entrada;

            //AL CIFRAR EL CONTENIDO, NOS DEVUELVE BYTES DE SALIDA
            byte[] salida;

            //NECESITAMOS UNA CLASE QUE NOS PERMITA CONVERTIR DE STRING A BYTE Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS UN OBJETO PARA CIFRAR EL CONTENIDO
            SHA1 managed = SHA1.Create();

            entrada = encoding.GetBytes(contenido);
            //LOS OBJETOS PARA CIFRAR CONTIENEN UN METODO LLAMADO COMPUTEHASH
            //QUE RECIBE UN ARRAY DE BYTES Y DVUELVEN OTRO ARRAY DE BYTES
            salida = managed.ComputeHash(entrada);
            //CONVERTIMOS SALIDA A STRING 
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
