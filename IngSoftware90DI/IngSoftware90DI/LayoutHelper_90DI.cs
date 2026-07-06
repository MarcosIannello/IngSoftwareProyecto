namespace UI_90DI
{
    // Helper de layout para pantallas completas. Garantiza que el
    // contenido del form se vea en cualquier resolución/DPI: lo centra en la pantalla,
    
    public static class LayoutHelper_90DI
    {
        public static void AjustarAPantalla_90DI(Form form)
        {
            // Si el contenido no entra en el área visible, aparecen scrollbars
            // (nada queda inaccesible) en vez de recortarse.
            form.AutoScroll = true;

            var area = Screen.FromControl(form).WorkingArea;

            // El form nunca puede ser más grande que el área de trabajo del monitor.
            form.MaximumSize = area.Size;
            if (form.Width > area.Width || form.Height > area.Height)
                form.Size = new Size(
                    Math.Min(form.Width, area.Width),
                    Math.Min(form.Height, area.Height));

            // Evita el maximizado (que corta contenido en pantallas chicas) y centra.
            form.WindowState = FormWindowState.Normal;
            form.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
