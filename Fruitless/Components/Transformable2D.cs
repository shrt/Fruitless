﻿using OpenTK;

namespace Fruitless.Components {
    /// <summary>
    /// Provides spatial transformation in a 2D-space (i.e. position, rotation and scale).
    /// </summary>
    public class Transformable2D : TransformationComponent {
        Vector2 _position = Vector2.Zero;
        Vector2 _scale = Vector2.One;

        float _rotationInRadians = 0;

        /// <summary>
        /// Gets the local transformation.
        /// This is the transformation relative to its parent.
        /// </summary>
        public override Matrix4 Local {
            get {
                return
                    Matrix4.CreateScale(Scale.X, Scale.Y, 1) *
                    Matrix4.CreateRotationZ(_rotationInRadians) *
                    Matrix4.CreateTranslation(Position.X, Position.Y, 0);
            }
        }

        /// <summary>
        /// Returns the distance in pixels between this and another `Transformable2D`.
        /// </summary>
        public float DistanceTo(Transformable2D other) {
            if (other == null) {
                return float.NaN;
            }

            return (Position - other.Position).Length;
        }

        /// <summary>
        /// The angle of rotation in radians.
        /// </summary>
        public float Rotation {
            get {
                return _rotationInRadians;
            }
            set {
                if (_rotationInRadians != value) {
                    _rotationInRadians = value;

                    RequiresWorldResolution = true;
                }
            }
        }

        /// <summary>
        /// The angle of rotation in degrees.
        /// </summary>
        public float RotationInDegrees {
            get {
                return OpenTK.MathHelper.RadiansToDegrees(Rotation);
            }
            set {
                Rotation = OpenTK.MathHelper.DegreesToRadians(value);
            }
        }
        
        /// <summary>
        /// Defaults to Vector2.One (1, 1).
        /// </summary>
        public Vector2 Scale {
            get {
                return _scale;
            }
            set {
                if (_scale != value) {
                    _scale = value;

                    RequiresWorldResolution = true;
                }
            }
        }

        /// <summary>
        /// The position in pixels relative to its parent.
        /// </summary>
        public Vector2 Position {
            get {
                return _position;
            }
            set {
                if (_position != value) {
                    _position = value;

                    RequiresWorldResolution = true;
                }
            }
        }
        
        /// <summary>
        /// The absolute position in pixels.
        /// </summary>
        public Vector2 AbsolutePosition {
            get {
                return new Vector2(World.M41, World.M42);
            }
        }
    }
}
