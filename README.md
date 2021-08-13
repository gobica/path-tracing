# Intruduction
The goal of this project is to get familiar with path tracing and the use of different light sources and materials in physically based rendering. Implamantation of basic path tracing algorithm, spherical lights and the Oren-Nayar material.

![alt text](https://github.com/gobica/path-tracing/blob/main/picture_103SSP.PNG)

## Path tracing 

The framework is developed in C# and includes basic math and support for easier implementation of a path tracer. It is loosely basedon the PBRTv3 rendering framework, developed by authors of book Physically Based Rendering.

The main path tracing method in the framework (within PathTracer.cs), which uses Russian
roulette for stopping and importance sampling for choosing ray directions.

## Lights

The framework supportsr spherical light sources. The source of the light is a surface of a
sphere positioned within the scene. The user is be able to set the radius of the sphere and the side of light emission (outside, inside) with uniform sampling of light rays from the surface.


## Materials
Framework implements the Oren-Nayar material in the framework. The template file is Lambertian.cs. 


