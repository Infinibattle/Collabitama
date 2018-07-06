namespace OnitamaTestClient.Models {
    public struct Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        public void Up(int up) {
            this.Y += up;
        }

        public void Down(int down) {
            this.Y -= down;
        }

        public void Left(int left) {
            this.X -= left;
        }

        public void Right(int right) {
            this.X += right;
        }

        public static Position operator +(Position a, Position b) {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b) {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public override bool Equals(object obj) {
            try {
                var position = (Position)obj;
                return this.X == position.X && this.Y == position.Y;
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
                return (this.X*397) ^ this.Y;
            }
        }

        public override string ToString() {
            return $"{{X: {this.X} Y: {this.Y} }}";
        }

        public static implicit operator Position((int x, int y) vector) {
            return new Position(vector.x, vector.y);
        }
    }
}
