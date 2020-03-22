using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;
using SQLite;

namespace _03_BaseDatosSQLite
{
    public class Persona
    {
        public Persona()
        {

        }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nombre { get; set; }
    }

    public class Auxiliar
    {
        static object locker = new object();
        SQLiteConnection conexion;

        public Auxiliar()
        {
            conexion = Conectar();
            conexion.CreateTable<Persona>();
        }

        public SQLite.SQLiteConnection Conectar()
        {
            SQLiteConnection conexionAuxiliar;
            string nombreArchivo = "BaseDatosSQLite.db3";
            string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string rutaCompleta = Path.Combine(ruta, nombreArchivo);
            conexionAuxiliar = new SQLiteConnection(rutaCompleta);
            return conexionAuxiliar;
        }

        //Seleccionar todo
        public IEnumerable<Persona> SeleccionarTodo()
        {
            lock (locker)
            {
                return (from i in conexion.Table<Persona>() select i).ToList();
            }
        }
        //Seleccionar un registro
        public Persona Seleccionar(string Nombre)
        {
            lock (locker)
            {
                return conexion.Table<Persona>().FirstOrDefault(x => x.Nombre == Nombre);
            }
        }
        //Actualizar o insertar
        public int Guardar(Persona registro)
        {
            lock (locker)
            {
                if(registro.ID == 0)
                {
                    return conexion.Insert(registro);
                }
                else
                {
                    return conexion.Update(registro);
                }
            }
        }
        //Eliminar
        public int Eliminar(int ID)
        {
            lock (locker)
            {
                return conexion.Delete<Persona>(ID);
            }
        }
    }
}