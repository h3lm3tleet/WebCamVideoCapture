namespace WebCamVideoCapture
{
    public static class WebCamFilters
    {
        public static bool isGrayFiltered, isNegativeFiltered, isResetFilters = false;
       
        public static string FilterSelector()
        {
            if (isGrayFiltered)
            {
                isNegativeFiltered = false;
                
                return "isGrayFiltered";
            }

            if (isNegativeFiltered)
            {
                isGrayFiltered = false;

                return "isNegativeFiltered";
            }

            else return "isResetFilters";
        }
    }
}
