/*
Saves the current Photoshop file in various formats:
	- GIF with dark background in \gif-dark
	- GIF with light background in \gif-light
	- JPEG with dark background in \jpeg-dark
	- JPEG with light background in \jpeg-light
	- PNG in \png
--------------------------------------------------------------------------------
*/

/*
File saving options.
--------------------------------------------------------------------------------
*/

// Options for saving JPEG images with light background.
jpegLight = new JPEGSaveOptions();
jpegLight.embedColorProfile = false;
jpegLight.formatOptions = FormatOptions.STANDARDBASELINE;
jpegLight.quality = 12;
jpegLight.matte = MatteType.WHITE;

// Options for saving JPEG images with dark background.
jpegDark = new JPEGSaveOptions();
jpegDark.embedColorProfile = false;
jpegDark.formatOptions = FormatOptions.STANDARDBASELINE;
jpegDark.quality = 12;
jpegDark.matte = MatteType.BLACK;

// Options for saving GIF images with light background.
gifLight = new GIFSaveOptions();
gifLight.colors = 256;
gifLight.dither = Dither.NONE;
gifLight.forced = ForcedColors.BLACKWHITE;
gifLight.interlaced = false;
gifLight.palette = Palette.LOCALPERCEPTUAL;
gifLight.preserverExactColors = true;
gifLight.transparency = true;
gifLight.matte = MatteType.WHITE;

// Options for saving GIF images with dark background.
gifDark = new GIFSaveOptions();
gifDark.colors = 256;
gifDark.dither = Dither.NONE;
gifDark.forced = ForcedColors.BLACKWHITE;
gifDark.interlaced = false;
gifDark.palette = Palette.LOCALPERCEPTUAL;
gifDark.preserverExactColors = true;
gifDark.transparency = true;
gifDark.matte = MatteType.BLACK;

// Options for saving PNG images.
png = new PNGSaveOptions();
png.interlaced = false;

/*
Array of destination file names with the appropriate save options.
--------------------------------------------------------------------------------
*/

function imageInfo (format, folderName, fileExtension)
{
  this.format = format;
  this.folderName = folderName;
  this.fileExtension = fileExtension;
}

var imageFormats = new Array(5);
imageFormats[0] = new imageInfo(jpegLight, 'jpeg-light', 'jpeg');
imageFormats[1] = new imageInfo(jpegDark, 'jpeg-dark', 'jpeg');
imageFormats[2] = new imageInfo(gifLight, 'gif-light', 'gif');
imageFormats[3] = new imageInfo(gifDark, 'gif-dark', 'gif');
imageFormats[4] = new imageInfo(png, 'png', 'png');

/*
Saving routine.
--------------------------------------------------------------------------------
*/

// Save the current view in all possible image formats.
for (format = 0; format < imageFormats.length; format++)
{
	try
	{
		// File name: "[PSD File Folder]\[Image Format]-[Background]\[PS Filename] [Width]x[Height].[Image Format Extension]".
		var file = new File(app.activeDocument.fullName.parent.fsName + '\\' +
			imageFormats[format].folderName + '\\' +
			stripExtension(app.activeDocument.name) + ' ' +
			app.activeDocument.width.as("px") + 'x' +
			app.activeDocument.height.as("px") + '.' +
			imageFormats[format].fileExtension);

		createFolder(file.fsName);

		app.activeDocument.saveAs(file, imageFormats[format].format, true, Extension.LOWERCASE);
	}
	catch(e)
	{
		alert("Error saving file.\n" + e)
	}
}

// Returns the image name without extension.
function stripExtension(filename)
{
	var extension = /\.(\w*)$/;
	filename = filename.replace(extension, '');
	return filename;
}

// Creates a folder for the destination file.
function createFolder(filename)
{
	var folder = new Folder(filename);

	// folder.parent holds the parent folder of the filename.
	if (!folder.parent.exists)
	{
		folder.parent.create();
	}
}
