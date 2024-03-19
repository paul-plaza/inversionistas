namespace Investors.Shared.Domain
{
    public static class ValidationConstants
    {
        /// <summary>
        /// {0} debe tener mínimo {1} caracteres y máximo {2} caracteres
        /// </summary>
        private const string ValidationMinAndMaxLength = "{0} debe tener mínimo {1} caracteres y máximo {2} caracteres";

        /// <summary>
        /// {0} debe estar entre: {1} y {2}
        /// </summary>
        private const string ValidationMinAndMaxDate = "{0} debe estar entre: {1} y {2}";

        /// <summary>
        /// {0} debe tener mínimo {1} caracteres
        /// </summary>
        private const string ValidationMinLength = "{0} debe tener mínimo {1} caracteres";

        /// <summary>
        /// {0} debe tener máximo {2} caracteres
        /// </summary>
        private const string ValidationMaxLength = "{0} debe tener máximo {2} caracteres";

        private const string ValidationIsRequired = "{0} campo requerido";


        public static string ValidateMaxLength(string parameterName, int maxLength)
        {
            return string.Format(ValidationMaxLength, parameterName, maxLength);
        }

        public static string ValidateMinLength(string parameterName, int maxLength)
        {
            return string.Format(ValidationMinLength, parameterName, maxLength);
        }

        public static string ValidateIsRequired(string parameterName)
        {
            return string.Format(ValidationIsRequired, parameterName);
        }

        /// <summary>
        /// {0} debe tener mínimo {1} caracteres y máximo {2} caracteres
        /// </summary>
        /// <param name="parameterName">Nombre a mostrar en validación</param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string ValidateMaxAndMinLength(string parameterName, int minLength, int maxLength)
        {
            return string.Format(ValidationMinAndMaxLength, parameterName, minLength, maxLength);
        }

        /// <summary>
        /// {0} debe estar entre: {1} y {2}
        /// </summary>
        /// <param name="parameterName">Nombre a mostrar en validación</param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public static string ValidateMaxAndMinLength(string parameterName, DateOnly minDate, DateOnly maxDate)
        {
            return string.Format(ValidationMinAndMaxDate, parameterName, minDate, maxDate);
        }

    }
}