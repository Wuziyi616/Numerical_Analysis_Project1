# Image Morphing Program

&#8195;&#8195;This project is the first task for THU DA 2019 Fall Numerical Analysis Project 1, an image morphing program that can rotate and distort input image to any degrees and radius.

## Intro

&#8195;&#8195;It's not difficult about the coordinate transform algorithm. For example in rotation, you just use a rotation matrix to get the original coordinate for an after-transform point. Actually the main purpose for this project is **interpolation** algorithms rather than transform.  

&#8195;&#8195;I implement three interpolation methods in the code, which are:

- Nearest
- Bilinear
- Bicubic  

&#8195;&#8195;You can select the method to use in UI and see the result by yourself.  

## Prerequisites
&#8195;&#8195;The project is developed in C#, .NET Framework 4.6.1 using Visual Studio 2017. Also I use opencvsharp package for some image processing operations.  
&#8195;&#8195;If you have VS installed on your PC (which I fully recommend), you can directly go to Tools --> NuGet --> NuGet Package Manager for this Project and search for OpenCvSharp3-AnyCPU.  
&#8195;&#8195;Of course, the package has been uploaded by me to this folder so maybe you can directly build this project once you git clone it.  

## Quick Start
&#8195;&#8195;To get a quick start of this repo, I recommend you to open the ImageMorphing.sln in VS then run it! You will see the UI like:  
  
<img src="https://github.com/Wuziyi616/Numerical_Analysis_Project1/blob/master/ImageMorphing/ImageMorphing/images/UI.png" width=600 alt="UI">
  
  
&#8195;&#8195;So let me give a brief description of this UI:
- You can select rotation or distortion in Transform Type
- You can set the interpolation method including nearest, bilinear and bicubic
- You can input an angle for rotation. Note that this parameter won't be used in distortion mode. However, in distortion mode, there exists two
distortion method which are convex and concave, so if you set angle as a positive value, I will recognize it as convex while a negative value means concave
(I will show examples later to clarify this)
- You can select a radius in Transform Radius. It will affect the result of output a lot
- You can input a path-to-image and also save the result to a specific path
- After you have finished all above, click Transform and see the result!  

## Results
&#8195;&#8195;More results can be viewed in ./ImageMorphing/images/  
Rotate 30 degrees using nearest:  
  
<img src="https://github.com/Wuziyi616/Numerical_Analysis_Project1/blob/master/ImageMorphing/ImageMorphing/images/THU_rotation_nearest_30.jpg" width=400 alt="ex1">  
  
Rotate 120 degrees using bicubic:  
  
<img src="https://github.com/Wuziyi616/Numerical_Analysis_Project1/blob/master/ImageMorphing/ImageMorphing/images/THU_rotation_bicubic_120.jpg" width=400 alt="ex2">  
  
Distort 30 degree using bilinear (**convex** distortion):  
  
<img src="https://github.com/Wuziyi616/Numerical_Analysis_Project1/blob/master/ImageMorphing/ImageMorphing/images/THU_distortion_bilinear_30.jpg" width=400 alt="ex3">  
  
Distort -30 degree using bilinear (**concave** distortion):  
  
<img src="https://github.com/Wuziyi616/Numerical_Analysis_Project1/blob/master/ImageMorphing/ImageMorphing/images/THU_distortion_bilinear_-30.jpg" width=400 alt="ex4">  
  
