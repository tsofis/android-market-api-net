## Requirements ##

  * This library uses Marc Gravell's [protobuf-net](http://code.google.com/p/protobuf-net/) library for serialization and deserialization.
  * For unzipping Google response streams, the [ICSharpCode SharpZipLib](http://www.icsharpcode.net/opensource/sharpziplib/) is used.

## Credits ##

This .NET adaptation is based almost entirely on the [Java android-market-api](http://code.google.com/p/android-market-api/), although there are some major strucutral differences in implementation.

## Proto modifications ##

Although all the Market API entities are originally auto-generated from the `market.proto` file, using Mark Gravell's serializer, changes have been made to the classes, that would be lost upon a complete re-generation.

  * All members have been converted to a .NET compliant naming convention, with regards to casing. If there's a switch in Marc's serializer, that does this automatically, I'd be happy to hear about it.
  * Aside from the casing differences, `AppsResult`'s member `app` has been changed to `Apps`, as it is a collection
  * The `StartIndex` property of `AppsRequest` and `CommentsRequest` has been set to required, to force them to serialize even when their values are `0`. This project is still in its nascency, and the only parts of it that is tested, are the ones that I've personally needed. It may well be the case that this change needs to be done in other classes as well.