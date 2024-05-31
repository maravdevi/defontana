public class NodoArbol
{
    public Nodo? Nodo { get; set; }
    public List<NodoArbol>? Hijos { get; set; }

    private static List<Familiar>? _listaPresentar = new();

    /// <summary>
    /// Método Recursivo: construye una estructura que facilita la presentación en pantalla del arbol.
    /// </summary>
    /// <param name="padre">representa el nodo padre que realizó la llamada sobre esta instancia.</param>
    /// <param name="nivelAnterior">Lleva el seguimiento del las veces que se ha llamado el método recursivamente.</param>
    /// <returns>Lista de objetos 'Familiar' utilizado representar el arbol en pantalla.</returns>
    public List<Familiar> Recorrer(NodoArbol? padre = null, int nivelAnterior = 1)
    {
        var nivelActual = nivelAnterior++;
        if(padre == null)
            _listaPresentar!.Add(new Familiar(){ Nombre = Nodo?.Nombre, Nivel = 1 , Padre = null});
        else
            _listaPresentar!.Add(new Familiar(){ Nombre = $"{Nodo?.Nombre}", Nivel = nivelActual, Padre = padre?.Nodo?.Nombre });

        for(int i = 0; i < Hijos!.Count; i++)
            Hijos[i].Recorrer(this, nivelAnterior);
        
        return _listaPresentar!.ToList();
    }
}