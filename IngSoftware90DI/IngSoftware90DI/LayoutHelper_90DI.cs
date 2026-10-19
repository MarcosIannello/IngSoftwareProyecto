namespace UI_90DI
{
    // Para que los forms de pantalla completa se vean bien en cualquier resolución o DPI:
    // los achica si no entran, los centra y agrega scroll si hace falta.
    
    public static class LayoutHelper_90DI
    {
        public static void AjustarAPantalla_90DI(Form form)
        {
            // Si el contenido no entra, que aparezcan scrollbars en vez de que se corte.
            form.AutoScroll = true;

            var area = Screen.FromControl(form).WorkingArea;

            // El form nunca puede ser más grande que el área de trabajo del monitor.
            form.MaximumSize = area.Size;
            if (form.Width > area.Width || form.Height > area.Height)
                form.Size = new Size(
                    Math.Min(form.Width, area.Width),
                    Math.Min(form.Height, area.Height));

            // Nada de maximizar (en pantallas chicas corta contenido), lo centramos y listo.
            form.WindowState = FormWindowState.Normal;
            form.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
