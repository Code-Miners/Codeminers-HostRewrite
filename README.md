# Codeminers Host Rewrite Module
This is a simple rewrite module for websites that can't respond to forwarded host headers. For example, you are on a Paas instance fronted by a WAF and you have to respond to multiple domain names.

Ordinarily you get the hostname as the normal hostname of your Paas instance and other domains come as forwarded hosts in the header collection. This module will attempt to find these and overwrite the incoming host value.

Note that the use of this module is extremely specific.

## Configuration

To configure the module, simply add it to the web.config as show below:

````xml
<add name="HostRewriteModule" type="Core.Modules.Web.HttpModules.HostRewriteModule, Codeminers.Core.Modules.Web.ForwardedHostRewriteModule" />
````
