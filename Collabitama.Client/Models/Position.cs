namespace Collabitama.Client.Models {
    public struct Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }

        public void Up(int up) {
            Y += up;
        }

        public void Down(int down) {
            Y -= down;
        }

        public void Left(int left) {
            X -= left;
        }

        public void Right(int right) {
            X += right;
        }

        public static Position operator +(Position a, Position b) {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b) {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public override bool Equals(object obj) {
            try {
                var position = (Position) obj;
                return X == position.X && Y == position.Y;
            }
            catch {
                return false;
            }
        }

        public static bool operator ==(Position a, Position b) {
            return a.Equals(b);
        }

        public static bool operator !=(Position a, Position b) {
            return !a.Equals(b);
        }

        public override int GetHashCode() {
            unchecked {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString() {
            return $"{{X: {X} Y: {Y} }}";
        }

        public static implicit operator Position((int x, int y) vector) {
            return new Position(vector.x, vector.y);
        }
    }
}