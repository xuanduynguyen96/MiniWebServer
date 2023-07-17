# Configuration
In appsettings.json file, please add one setting which is Jwt's secret key, following below format:
{
  JwtOptions: {
    Key: "<your desired secret key>"    
  }
}

IMPORTANT NOTE:
  * You should store secret keys in a secured storage like cloud's key vault or something like that, read some article to know more about this.
  * With development environment and visual studio as coding tool, you can manage to store secret keys in secrets.json file (once again you can search on internet, there's article about this, just some few clicks to setup).
