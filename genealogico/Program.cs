class Program
{
    /// <summary>
    /// Datos de entrada del problema
    /// </summary>
    static List<Nodo> listaNodos = new List<Nodo>
        {
            new Nodo{ Id = 2,  Nombre = "Hermano", IdPadre = 4,  },
            new Nodo{ Id = 3,  Nombre = "Hermana", IdPadre = 4,  },
            new Nodo{ Id = 4,  Nombre = "Madre",   IdPadre = 10, },
            new Nodo{ Id = 1,  Nombre = "Yo",      IdPadre = 4,  },
            new Nodo{ Id = 5,  Nombre = "Tía",     IdPadre = 10, },
            new Nodo{ Id = 6,  Nombre = "Prima",   IdPadre = 5,  },
            new Nodo{ Id = 7,  Nombre = "Primo",   IdPadre = 8,  },
            new Nodo{ Id = 8,  Nombre = "Tío",     IdPadre = 10, },
            new Nodo{ Id = 9,  Nombre = "Sobrino", IdPadre = 3,  },
            new Nodo{ Id = 10, Nombre = "Abuela",  IdPadre = 0,  }
        };

    static readonly NodoArbol? nodoRaiz;

    /// <summary>
    /// Constructor
    /// </summary>
    static Program()
    {
        nodoRaiz = listaNodos.Where(x => x.IdPadre == 0).Select(x => new NodoArbol() { Nodo = x }).ToList()[0];
    }

    /// <summary>
    /// Punto de entrada de la aplicación.
    /// </summary>
    static void Main()
    {
        var nivel = 0;
        var nodosNivel = new List<NodoArbol>() { nodoRaiz! };

        GenerarArbol(nivel, nodosNivel, null);
        RecorrerArbol();
        Console.ReadKey();
    }

    /// <summary>
    /// Metodo Recursivo: Genera el arbol de nodos a partir de la lista listaNodos.
    /// </summary>
    /// <param name="nivel">Nivel de profundidad en el arbol</param>
    /// <param name="nodosNivel">Nodos encontrados que pertenecen a un mismo nivel.</param>
    /// <param name="nodoPadre">Nodo padre de los nodos de este nivel.</param>
    static void GenerarArbol(int nivel, List<NodoArbol>? nodosNivel, NodoArbol? nodoPadre)
    {
        nivel++;
        for (int i = 0; i < nodosNivel?.Count; i++)
        {
            var nodo = nodosNivel[i];
            var currId = nodo?.Nodo?.Id;
            var hijosNodo = listaNodos.Where(x => x.IdPadre == currId).Select(x => new NodoArbol() { Nodo = x }).ToList();
            nodosNivel[i].Hijos = hijosNodo;
            GenerarArbol(nivel, hijosNodo, nodo);
        }
    }

    /// <summary>
    /// Metodo que representa en pantalla el arbol generado. 
    /// La representación se crea por medio del nodo raiz 'nodoRaiz?.Recorrer()'.
    /// </summary>
    static void RecorrerArbol()
    {
        var result = from f in nodoRaiz?.Recorrer() group f by f.Nivel into grp
                        orderby grp.Key select new { Nivel = grp.Key, Miembros = grp };

        foreach(var r in result!)
        {
            Console.WriteLine($"{r.Nivel}");
            foreach(var f in r.Miembros)
            {
                var padre = f.Padre != null? $"hijo de {f.Padre}": "raiz";
                Console.WriteLine(string.Format("{0,-15} ({1})", f.Nombre, padre));
            }
            Console.WriteLine();
        }
    }
}