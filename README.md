# PadBinaryFile
Pad a binary with $FF to a set size

This software will read a binary file and pad it with FF bytes to a nominated size.

Nothing fancy and super simple, written for .Net 4 in C#. I needed tod this for a microcontroller project and all of the 
standard ways were too fussy. As this is trivial code we are making it open to the public under the Apache 2.0 licence.

usage: PadBinaryFile <inputfile> <output file> <final size as a HEX value, eg FFFF>
