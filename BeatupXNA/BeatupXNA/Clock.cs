namespace BeatupXNA
{
    public class Clock
    {
        private float time_elapsed;
        private float threshold;

        public void reset() { time_elapsed = 0; }
        public void set_threshold(float threshold) { this.threshold = threshold; }
        public bool passed_threshold() { return this.time_elapsed >= threshold; }

        public float get_percentage() {
            if (this.time_elapsed <= 0.0f) { return 0.0f; }
            else { return this.time_elapsed / this.threshold; };
        }
        public bool is_started() { return this.time_elapsed > 0; }
        public bool is_active() { return this.is_started() && !this.passed_threshold(); }

        public void start() { time_elapsed = 0.001f; } //figure out a better way to do this
        public void start_for_thres(float val) { this.set_threshold(val); this.start(); }
    };
}