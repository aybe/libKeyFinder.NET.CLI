# libKeyFinder.NET.CLI
Key chord detection command-line utility for Windows

Supports : WAV, FLAC, Ogg/Vorbis

## Requirements

[Microsoft Visual C++ Redistributable for Visual Studio 2017](https://www.visualstudio.com/downloads/)

[.NET Framework](https://www.visualstudio.com/downloads/)

## Download

https://github.com/aybe/libKeyFinder.NET.CLI/releases

## Usage

```
libKeyFinder.NET 1.0.0.0
Copyright c  2017

ERROR(S):
  Required option 'path, p' is missing.


  -p, --path         Required. Path to an audio file

  -b, --blocksize    (Default: 1024) Block size in samples

  --help             Display this help screen.

Return codes:
  Help         : -2
  Error        : -1
  AMajor       : 0
  AMinor       : 1
  BFlatMajor   : 2
  BFlatMinor   : 3
  BMajor       : 4
  BMinor       : 5
  CMajor       : 6
  CMinor       : 7
  DFlatMajor   : 8
  DFlatMinor   : 9
  DMajor       : 10
  DMinor       : 11
  EFlatMajor   : 12
  EFlatMinor   : 13
  EMajor       : 14
  EMinor       : 15
  FMajor       : 16
  FMinor       : 17
  GFlatMajor   : 18
  GFlatMinor   : 19
  GMajor       : 20
  GMinor       : 21
  AFlatMajor   : 22
  AFlatMinor   : 23
  Silence      : 24

Credits :
  https://github.com/ibsh/libKeyFinder
  https://github.com/aybe/libKeyFinder.NET
  https://github.com/erikd/libsndfile
```

### Notes

- the `blocksize` parameter does not affect the detection, just how many samples are sent each time to the detector
- return codes allows batch processing, e.g. with `for /f` and `echo %errorlevel%`
- it takes about 4 seconds to detect the key of a 7 minutes stereo FLAC file on a Core i7
- it uses the latest version of [libKeyFinder](https://github.com/ibsh/libKeyFinder) with default settings
- the finale key is the one the algorithm decided is the best candidate, pre-completion ones are just eye-candy
- this utility is solely an easy to use convenience for musicians looking for a free key detector [with good accuracy](http://ibrahimshaath.co.uk/keyfinder/comparison.pdf), if you need more control such as progressive key report, craft your own app with [libKeyFinder.NET](https://github.com/aybe/libKeyFinder.NET) below


## Links

https://github.com/ibsh/libKeyFinder <- author of the original library

https://github.com/aybe/libKeyFinder <- fork with CMakeLists and C wrapper

https://github.com/aybe/libKeyFinder.NET <- .NET library + NuGet package

https://github.com/erikd/libsndfile <- THE library to read audio files
