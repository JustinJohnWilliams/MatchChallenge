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

let split sep (x : String) = x.Split([|sep|])

let next _ =
  click "Continue"

let keepGoing _ =
  click ".progress-next"

let signOut _ =
  hover "N"
  click "Sign Out "
  browser.Manage().Cookies.DeleteAllCookies()

let random n =
  Guid.NewGuid().ToString().Substring(0, n)

let openBrowser _ =
  configuration.chromeDir <- "./"
  let options = Chrome.ChromeOptions()
  options.AddArgument("--enable-logging")
  options.AddArgument("--v=0")
  start (ChromeWithOptions options)
  browser.Manage().Cookies.DeleteAllCookies()

let mutable siteType = 1
let mutable myFavorite = "some favorite"
let mutable email = random 5 + "@gmail.com"
let mutable username = random 5
let mutable password = random 5

let assignSiteType _ =
  sleep 5
  printfn "%d" siteType
  printfn "%s" ( currentUrl() )
  siteType <- if exists "#topspot-dash" then 2 else 1
  printfn "%d" siteType

let init _ =
  email <- random 5 + "@gmail.com"
  username <- random 5
  password <- random 5
  ()

let createAccount _ =
  url "http://www.match.com"
  click "Member Sign In »"
  url "http://www.match.com/registration/registration.aspx"
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
  url "http://www.match.com"

let addFavorite _ =
  url "http://www.match.com"
  on "/home/mymatch.aspx"
  let firstMatch = first ".option"
  myFavorite <- firstMatch.Text.Split(' ').[0].Split('\n').[0]
  click myFavorite
  click ".cta-favorite"
  on "/matchbook/AddEntry.aspx"

let closeEmailAlert _ =
  if exists ".notif-email" then
     let element1 = element ".notif-email"
     if element1.Displayed then click "Later"

  if exists "#notificationEmail" then
     let modal2 = element "#notificationEmail"
     if modal2.Displayed then click "continue to site"

let signIn _ =
  browser.Manage().Cookies.DeleteAllCookies()
  url "http://www.match.com"
  click "Member Sign In »"
  assignSiteType()
  "#email" << email
  "#password" << password
  click "Sign in now »"
  closeEmailAlert()
  if siteType = 1 then on "/home/mymatch.aspx" else on "/MyMatch/Index"

let verifyFavorite _ =
  click "F"
  closeEmailAlert()
  if siteType = 2 then on "interests/fave/"
  click "my faves "
  displayed ".cards"
  displayed myFavorite

openBrowser()
init()
createAccount()
signOut()
signIn()
addFavorite()
signOut()
signIn()
verifyFavorite()
signOut()
