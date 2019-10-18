# Face Morphing Program

&#8195;&#8195;This project is the second task for THU DA 2019 Fall Numerical Analysis Project 1, an face morphing program that transform between human faces using TPS transform based on face-landmarks.  

## Intro
&#8195;&#8195;The coordinate transform algorithm for this project is TPS (Thin Plate Spline), which compute a transfer vector based on two sets of points.
The function of this algorithm is to do non-rigid transform (for example rotation, distortion) for an image, which can't be demonstrated by a transform matrix or regular formula.
The method is to minimize an energy function which has analytical solution. For detailed information about the TPS algorithm, I recommend you to go to [Wiki-Thin plate spline](https://en.wikipedia.org/wiki/Thin_plate_spline).  

## Prerequisites
&#8195;&#8195;The project is developed in C#, .NET Framework 4.6.1 using Visual Studio 2017. Other packages include:
- OpenCvSharp3-AnyCPU.3.4.1.20180319
- DlibDotNet.19.18.0.20190928  
You can simply install these two packages via NuGet package maneger in Visual Studio.  

## Quick Start
&#8195;&#8195;To get a quick start of this repo, I recommend you to open the FaceMorphing.sln in VS then run it! You will see the UI like:  
  
<img src="" width=600 alt="UI">
  
  
&#8195;&#8195;So let me give a brief description of this UI:
- You can select rotation or distortion in Transform Type
- You can set the interpolation method including nearest, bilinear and bicubic
- You can input an angle for rotation. Note that this parameter won't be used in distortion mode. However, in distortion mode, there exists two
distortion method which are convex and concave, so if you set angle as a positive value, I will recognize it as convex while a negative value means concave
(I will show examples later to clarify this)
- You can select a radius in Transform Radius. It will affect the result of output a lot
- You can input a path-to-image and also save the result to a specific path
- After you have finished all above, click Transform and see the result!  

## About the code
&#8195;&#8195;A simple description about the code files:
- Form1.cs is the UI file (and it's quite ugly OTZ)
- Task.cs is the class definition for task requirements, including angles, radius, methods, etc
- Transform.cs is the class for transform and interpolation algorithms
- Matrix2d.cs is my own implementation of a 2d matrix. That's because we are not allowed to use matrix class from packages like OpenCV or Eigen. So in this class I implement some simple operations like inverse, swap rows and columns, etc  

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
  
