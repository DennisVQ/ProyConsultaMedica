using System.Web;
using System.Web.Optimization;

namespace ProyConsultaMedica
{
    public class BundleConfig
    {

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            /* ----------------- STYLES CSS -------------------- */

            // Default
            bundles.Add(new StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css"));

            // Plantilla:
            bundles.Add(new StyleBundle("~/Content/css_plantilla").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/jquery-ui.min.css",
                "~/Content/animate.css",
                "~/Content/css-plugin-collections.css",
                // CSS | menuzord megamenu skins - En la plantilla el LINK tenia ID
                "~/Content/menuzord-skins/menuzord-boxed.css",
                // CSS | Main style file
                "~/Content/style-main.css",
                // CSS | Preloader Styles
                "~/Content/preloader.css",
                // CSS | Custom Margin Padding Collection
                "~/Content/custom-bootstrap-margin-padding.css",
                // CSS | Responsive media queries
                "~/Content/responsive.css",
                // Revolution Slider 5.x CSS settings
                "~/ Scripts/revolution-slider/css/settings.css",
                "~/Scripts/revolution-slider/css/layers.css",
                "~/Scripts/revolution-slider/css/navigation.css",
                // CSS | Theme Color
                "~/Content/colors/theme-skin-blue.css"
                ));


            /* ----------------- SCRIPTS JS -------------------- */

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Plantilla Header:
            bundles.Add(new ScriptBundle("~/bundles/js_plantilla_header").Include(
                      // external javascripts
                      "~/Scripts/jquery-2.2.4.min.js",
                      "~/Scripts/jquery-ui.min.js",
                      "~/Scripts/~/Scripts/bootstrap.min.js",
                      //  JS | jquery plugin collection for this theme
                      "~/Scripts/jquery-plugin-collection.js",
                      // Revolution Slider 5.x SCRIPTS
                      "~/Scripts/revolution-slider/js/jquery.themepunch.tools.min.js",
                      "~/Scripts/revolution-slider/js/jquery.themepunch.revolution.min.js"
                ));

            // Plantilla Footer:
            bundles.Add(new ScriptBundle("~/bundles/js_plantilla_footer").Include(
                    // JS | Custom script for all pages
                    "~/Scripts/custom.js",
                    // SLIDER REVOLUTION 5.0 EXTENSIONS  
                    // (Load Extensions only on Local File Systems ! The following part can be removed on Server for On Demand Loading)
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.actions.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.carousel.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.kenburn.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.layeranimation.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.migration.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.navigation.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.parallax.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.slideanims.min.js",
                    "~/Scripts/revolution-slider/js/extensions/revolution.extension.video.min.js"            
                    ));




        }
    }
}
