using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatting
{
    public class HTMLContent
    {
        #region General HTML format
        public static string OpenHTML
        {
            get
            {
                return @"<!DOCTYPE html>
                    <html lang=""en"">";
            }
        }

        public static string CloseHTML
        {
            get
            {
                return @"</html>";
            }
        }

        public static string HeaderHTML
        {
            get
            {
                return @"<head>
                        <meta charset=""utf-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
                        <meta name=""description"" content="""">
                        <meta name=""author"" content="""">
                        <link rel=""icon"" href=""../../../../favicon.ico"">

                        <title>Dynamics 365 for Finance and Operations technical design document</title>

                        <!-- Bootstrap core CSS -->
                        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css"" integrity=""sha384-/Y6pD6FV/Vv2HJnA6t+vslU6fwYXjCFtcEpHbNJ0lyAFsXTsjBbfaDjzALeQsN6M"" crossorigin=""anonymous"">

                        <!-- Custom styles for this template -->
                        <link href=""jumbotron.css"" rel=""stylesheet"">
                    </head>";
            }
        }

        public static string Header
        {
            get
            {
                return @"<nav class=""navbar navbar-expand-md navbar-dark fixed-top bg-dark"">
                            <a class=""navbar-brand"" href=""#"">Navbar</a>
                            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbarsExampleDefault"" aria-controls=""navbarsExampleDefault"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                                <span class=""navbar-toggler-icon""></span>
                            </button>

                            <div class=""collapse navbar-collapse"" id=""navbarsExampleDefault"">
                                <ul class=""navbar-nav mr-auto"">
                                    <li class=""nav-item active"">
                                        <a class=""nav-link"" href=""#"">Home <span class=""sr-only"">(current)</span></a>
                                    </li>
                                    <li class=""nav-item"">
                                        <a class=""nav-link"" href=""#"">Link</a>
                                    </li>
                                    <li class=""nav-item"">
                                        <a class=""nav-link disabled"" href=""#"">Disabled</a>
                                    </li>
                                    <li class=""nav-item dropdown"">
                                        <a class=""nav-link dropdown-toggle"" href=""http://example.com"" id=""dropdown01"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">Dropdown</a>
                                        <div class=""dropdown-menu"" aria-labelledby=""dropdown01"">
                                            <a class=""dropdown-item"" href=""#"">Action</a>
                                            <a class=""dropdown-item"" href=""#"">Another action</a>
                                            <a class=""dropdown-item"" href=""#"">Something else here</a>
                                        </div>
                                    </li>
                                </ul>
                                <form class=""form-inline my-2 my-lg-0"">
                                    <input class=""form-control mr-sm-2"" type=""text"" placeholder=""Search"" aria-label=""Search"">
                                    <button class=""btn btn-outline-success my-2 my-sm-0"" type=""submit"">Search</button>
                                </form>
                            </div>
                        </nav>

                        <!-- Main jumbotron for a primary marketing message or call to action -->
                        <div class=""jumbotron"">
                            <div class=""container"">
                                <h3 class=""display-4"">{0}</h3>
                                <p>{1}</p>
                                <p><a class=""btn btn-primary btn-lg"" href=""#"" role=""button"">Learn more &raquo;</a></p>
                            </div>
                        </div>";
            }
        }

        public static string OpenBody
        {
            get
            {
                return @"<body style=""padding-top: 3.5rem"">";
            }
        }

        public static string CloseBody
        {
            get
            {
                return @"<!-- Bootstrap core JavaScript
                        ================================================== -->
                        <!-- Placed at the end of the document so the pages load faster -->
                        <script src=""https://code.jquery.com/jquery-3.2.1.slim.min.js"" integrity=""sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"" crossorigin=""anonymous""></script>
                        <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"" integrity=""sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4"" crossorigin=""anonymous""></script>
                        <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js"" integrity=""sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1"" crossorigin=""anonymous""></script>
                    </body>";
            }
        }

        public static string Footer
        {
            get
            {
                return @"<footer><p>&copy; Company 2017</p></footer>";
            }
        }

        public static string OpenContainer
        {
            get
            {
                return @"<div class=""container"">";
            }
        }

        public static string CloseContainer
        {
            get
            {
                return @"</div> <!-- /container -->";
            }
        }

        public static string Content
        {
            get
            {
                return @" < !DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""utf-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
                        <meta name=""description"" content="""">
                        <meta name=""author"" content="""">
                        <link rel=""icon"" href=""../../../../favicon.ico"">

                        <title>Jumbotron Template for Bootstrap</title>

                        <!-- Bootstrap core CSS -->
                        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css"" integrity=""sha384-/Y6pD6FV/Vv2HJnA6t+vslU6fwYXjCFtcEpHbNJ0lyAFsXTsjBbfaDjzALeQsN6M"" crossorigin=""anonymous"">

                        <!-- Custom styles for this template -->
                        <link href=""jumbotron.css"" rel=""stylesheet"">
                    </head>

                    <body>

                        <nav class=""navbar navbar-expand-md navbar-dark fixed-top bg-dark"">
                            <a class=""navbar-brand"" href=""#"">Navbar</a>
                            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbarsExampleDefault"" aria-controls=""navbarsExampleDefault"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                                <span class=""navbar-toggler-icon""></span>
                            </button>

                            <div class=""collapse navbar-collapse"" id=""navbarsExampleDefault"">
                                <ul class=""navbar-nav mr-auto"">
                                    <li class=""nav-item active"">
                                        <a class=""nav-link"" href=""#"">Home <span class=""sr-only"">(current)</span></a>
                                    </li>
                                    <li class=""nav-item"">
                                        <a class=""nav-link"" href=""#"">Link</a>
                                    </li>
                                    <li class=""nav-item"">
                                        <a class=""nav-link disabled"" href=""#"">Disabled</a>
                                    </li>
                                    <li class=""nav-item dropdown"">
                                        <a class=""nav-link dropdown-toggle"" href=""http://example.com"" id=""dropdown01"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">Dropdown</a>
                                        <div class=""dropdown-menu"" aria-labelledby=""dropdown01"">
                                            <a class=""dropdown-item"" href=""#"">Action</a>
                                            <a class=""dropdown-item"" href=""#"">Another action</a>
                                            <a class=""dropdown-item"" href=""#"">Something else here</a>
                                        </div>
                                    </li>
                                </ul>
                                <form class=""form-inline my-2 my-lg-0"">
                                    <input class=""form-control mr-sm-2"" type=""text"" placeholder=""Search"" aria-label=""Search"">
                                    <button class=""btn btn-outline-success my-2 my-sm-0"" type=""submit"">Search</button>
                                </form>
                            </div>
                        </nav>

                        <!-- Main jumbotron for a primary marketing message or call to action -->
                        <div class=""jumbotron"">
                            <div class=""container"">
                                <h1 class=""display-3"">Hello, world!</h1>
                                <p>This is a template for a simple marketing or informational website. It includes a large callout called a jumbotron and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
                                <p><a class=""btn btn-primary btn-lg"" href=""#"" role=""button"">Learn more &raquo;</a></p>
                            </div>
                        </div>

                        <div class=""container"">
                            <!-- Example row of columns -->
                            <div class=""row"">
                                <div class=""col-md-4"">
                                    <h2>Heading</h2>
                                    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                                    <p><a class=""btn btn-secondary"" href=""#"" role=""button"">View details &raquo;</a></p>
                                </div>
                                <div class=""col-md-4"">
                                    <h2>Heading</h2>
                                    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                                    <p><a class=""btn btn-secondary"" href=""#"" role=""button"">View details &raquo;</a></p>
                                </div>
                                <div class=""col-md-4"">
                                    <h2>Heading</h2>
                                    <p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>
                                    <p><a class=""btn btn-secondary"" href=""#"" role=""button"">View details &raquo;</a></p>
                                </div>
                            </div>

                            <hr>

                            <footer>
                                <p>&copy; Company 2017</p>
                            </footer>
                        </div> <!-- /container -->
                        <!-- Bootstrap core JavaScript
                        ================================================== -->
                        <!-- Placed at the end of the document so the pages load faster -->
                        <script src=""https://code.jquery.com/jquery-3.2.1.slim.min.js"" integrity=""sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"" crossorigin=""anonymous""></script>
                        <script src=""https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"" integrity=""sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4"" crossorigin=""anonymous""></script>
                        <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js"" integrity=""sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1"" crossorigin=""anonymous""></script>
                    </body>
                    </html>
                    ";
            }
        }
        #endregion

        #region AxTable
        public static string TableTag
        {
            get
            {
                return @"<p><h4>{0} ({1})</h4></p>
                         <p><table class=""table table-striped"">
                            <caption>{2}</caption>
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Type</th>
                                    <th>Label/HelpText</th>
                                    <th>Changed properties</th>
                                </tr>
                                </thead>
                                <tbody>
                                    {3}
                                </tbody>
                            </table></p>";
            }
        }

        public static string TableFieldsTag
        {
            get
            {
                return @"<tr>
                            <td>{0}</td>
                            <td>{1} ({2})</td>
                            <td>{3}</td>
                            <td>{4}</td>
                        </tr>";
            }
        }
        #endregion
    }
}
