using System;
using System.Linq;
using System.Collections;
using static RPG.Clases;
using RPG;

namespace HelloWold;

public class Program
{
    public static int HPEnemy = 100;
    public static int ACEnemy = 12;
    public static string accionTurno = "";
    public static int estadistica = Clases.Guerrero.Strength;
    public static int XP = 0;
    public static bool ModoTexto;

    public static void Main()
    {
        SeleccionTexto();
        AccionJuego();
        AnimacionEscribir("Empieza el combate, tu enemigo cuenta con un AC de " + ACEnemy);

        while (HPEnemy > 0 && accionTurno != "3")
        {
            TurnoAliado();
        }
        XP += 100;
        LvlUp();
        AccionJuego();


    }
    public static void SeleccionTexto()
    {
        AnimacionEscribir("Desea que el texto sea:\n1.Normal\n2.Rapido");
        string select = Console.ReadLine();
        switch (select)
        {
            case "1":
                ModoTexto = true;
                break;
            case "2":
                ModoTexto = false;
                break;
        }
    }
    public static void SeleccionClase()
    {
        AnimacionEscribir("Escoga su clase: \n1.Guerrero \n2.Mago \n3.Sacerdote");
        accionTurno = Console.ReadLine();
        switch (accionTurno)
        {
            case "1":
                //generar seleccion y usar un get set para setear player = clase seleccionada
                break;
        }
    }
    public static void TurnoAliado()
    {

        AnimacionEscribir("Escoga la suguiente accion: \n1.Habilidades \n2.Mochila \n3.Huir");
        accionTurno = Console.ReadLine();
        switch (accionTurno)
        {
            case "1":
                ShowHP();
                MuestraHabilidades();
                AnimacionEscribir("------------------------------");
                break;
            default:
                AnimacionEscribir("Porfavor seleccione una de las opciones unicente ingesando el numero de la accion que desea realizar");
                break;
        }
    }
    public static void AccionJuego()
    {
        AnimacionEscribir("Cual será su siguiente acción?\n1.Batalla\n2.Terminar Partida");
        string accionJuego = Console.ReadLine();
        switch (accionJuego)
        {
            case "1":
                //crea nuevo enemigo, empieza la batlla
                break;
            case "2":
                //termina el run, muestra el puntaje
                break;
            default:
                AnimacionEscribir("Porfavor seleccione una de las opciones unicente ingesando el numero de la accion que desea realizar");
                AccionJuego();
                break;

        }
    }
    public static void MuestraHabilidades()
    {
        string ataque = "";
        AnimacionEscribir("1.Ataque basico \n2.Bola de fuego");
        ataque = Console.ReadLine();
        switch (ataque)
        {
            case "1":
                HPEnemy -= DmgDone();
                ShowHP();
                break;
            case "2":
                HPEnemy -= 10;
                ShowHP();
                break;
        }

    }
    public static void ShowHP()
    {
        AnimacionEscribir("vida enemiga = " + HPEnemy.ToString());
    }
    public static void ShowBag()
    {
        //mostrar consumibles
    }
    public static int ModificadorEstadisitca(int estadistica)
    {
        Dictionary<int, int> tablaValores = new Dictionary<int, int>()
            {
                { 0, -5 },
                { 2, -4 },
                { 4, -3 },
                { 6, -2 },
                { 8, -1 },
                { 10, 0 },
                { 12, 1 },
                { 14, 2 },
                { 16, 3 },
                { 18, 4 },
                { 20, 5 }
            };
        return tablaValores[estadistica];
    }
    public static bool RollAttack()
    {
        //Critico solo si sale 20 en este dado (x2 de dmg)
        int stat = ModificadorEstadisitca(estadistica);
        int Caras = Clases.Guerrero.RAttack;
        Random rnd = new Random();
        int num = rnd.Next(Caras);
        int numFinal = stat + num;
        if (numFinal >= ACEnemy)
        {
            AnimacionEscribir("Tiras los dados de ataque, sacas un: " + num + "\nJunto con tu modificador por estadistiacs el valor final es de: " + numFinal );
            return true;
        }
        else
        {
            AnimacionEscribir("Tiras los dados de ataque, sacas un: " + num + "\nJunto con tu modificador por estadistiacs el valor final es de: " + numFinal );
            return false;
        }

    }
    public static int DmgDone()
    {
        //primero se chequea si el golpe se acierta
        bool rollAtack = RollAttack();
        //calculo de daño en funcion de atributos de la clase
        if (rollAtack)
        {
            int stat = ModificadorEstadisitca(estadistica);
            int Caras = Clases.Guerrero.RAttack;
            Random rnd = new Random();
            int num = rnd.Next(Caras);
            int Dmg = num + stat;
            AnimacionEscribir("Ataque Acertado!\n\nTiras los dados de daño, sacas un: " + num + "\nJunto con tu modificador por estadistiacs el valor final es de: " + Dmg);

            return Dmg;
        }
        else
        {
            AnimacionEscribir("Ataque fallido");
            return 0;
        }

    }
    public static void AnimacionEscribir(string texto)
    {
        if (ModoTexto)
        {
            int velocidad = 30; // milisegundos
            for (int i = 0; i < texto.Length; i++)
            {
                Console.Write(texto[i]);
                if (i + 1 < texto.Length && texto[i + 1] == '*')
                {
                    Console.Write('*');
                }
                Thread.Sleep(velocidad);
            }
            Console.WriteLine("\n");
        }
        else
        Console.WriteLine(texto + "\n"); 
        
    }
    public static void LvlUp() 
    {
        if(HPEnemy == 0)
        {
            XP += 100;
            AnimacionEscribir("LVL UP!");
            HPEnemy = 100;
        }
    }
}