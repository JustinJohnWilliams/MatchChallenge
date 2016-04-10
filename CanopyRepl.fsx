#I "./FSharpModules/UnionArgParser/lib/net40"
#I "./FSharpModules/Microsoft.SqlServer.Types/lib/net20"
#I "./FSharpModules/FSharp.Data/lib/net40"
#I "./FSharpModules/FSharp.Data.SqlClient/lib/net40"
#I "./FSharpModules/Http.fs/lib/net40"
#I "./FSharpModules/Selenium.WebDriver/lib/net40"
#I "./FSharpModules/Selenium.Support/lib/net40"
#I "./FSharpModules/SizSelCsZzz/lib"
#I "./Fsharpmodules/Newtonsoft.Json/lib/net40"
#I "./FSharpModules/canopy/lib"
#I "./FsharpModules/Http.fs/lib/net40"

#r "UnionArgParser.dll"
#r "Microsoft.SqlServer.Types.dll"
#r "FSharp.Data.SqlClient.dll"
#r "HttpClient.dll"
#r "WebDriver.dll"
#r "WebDriver.Support.dll"
#r "HttpClient.dll"
#r "canopy.dll"
#r "System.Core.dll"
#r "System.Xml.Linq.dll"
#r "FSharp.Data.dll"

open HttpClient
open canopy
open runner
open System
open System.Collections.ObjectModel
open FSharp.Data
open Nessos.UnionArgParser
open types
open reporters
open configuration
open OpenQA.Selenium.Firefox
open OpenQA.Selenium
open OpenQA.Selenium.Support.UI
open OpenQA.Selenium.Interactions

let exists selector =
  let e = someElement selector
  match e with
    | Some(e) -> true
    | _ -> false

let next _ =
    click "Continue"

let random n =
    Guid.NewGuid().ToString().Substring(0, n)

let openBrowser _ =
    configuration.chromeDir <- "./"
    let options = Chrome.ChromeOptions()
    options.AddArgument("--enable-logging")
    options.AddArgument("--v=0")
    start (ChromeWithOptions options)
    browser.Manage().Cookies.DeleteAllCookies()

let email = random 5 + "@something.com"
let username = random 5
let password = random 5

openBrowser()

url "http://match.com"

click "Member Sign In »"
click "SUBSCRIBE"
//somethingk
"#genderGenderSeek" << "Man seeking a Woman"
"#postalCode" << "75034"
click "View Singles"
"[name='email']" << email
next ()
"[name='password']" << password
"#birthMonth" << "Dec"
"#birthDay" << "29"
"#birthYear" << "1987"
next ()
"[name='handle']" << username
next ()
on "/Profile/Create/Welcome/?" //logged in
