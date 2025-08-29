$(document).ready(function () {
  const section = document.querySelector(".ap-counters");

  const observer = new IntersectionObserver(
    (entries, observer) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          // Start the counter animations only when the section becomes visible
          $(".counter").each(function () {
            const $this = $(this);
            const targetValue = parseInt($this.text(), 10); // Ensure the target value is a number
            $this.text(0); // Reset counter to 0 before animation starts

            jQuery({ Counter: 0 }).animate(
              { Counter: targetValue },
              {
                duration: 1000,
                easing: "swing",
                step: function () {
                  $this.text(Math.ceil(this.Counter));
                },
              }
            );
          });

          observer.unobserve(entry.target); // Stop observing after animation starts
        }
      });
    },
    { threshold: 0.5 } // Trigger when 50% of the section is visible
  );

  if (section) {
    observer.observe(section);
  }
});
