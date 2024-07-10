
var defaultOptions = {
    threshold: 2,
    gap: 12,
    transition: false
};

    function DragSlider(selector, options) {
        this.isDown = false;
        this.options = Object.assign(({}, defaultOptions), options);
        this.selector = document.querySelector(selector);
        this.childSelector = this.selector.querySelector('.drag-slider');
        this.init();
    }
    DragSlider.prototype.init = function () {
        this.projectControlElements();
        this.eventRegistration();
        //this.controlVisibility();
    };
    DragSlider.prototype.projectControlElements = function () {
        this.leftOverlay = document.createElement('span');
        this.leftOverlay.className = 'scroll-overlay left';
        this.rightOverlay = document.createElement('span');
        this.rightOverlay.className = 'scroll-overlay right';
        this.selector.append(this.leftOverlay);
        this.selector.prepend(this.rightOverlay);
    };
    DragSlider.prototype.eventRegistration = function () {
        var _this = this;
        // Mouse Up Function
        this.childSelector.addEventListener("mouseup", function () {
            _this.isDown = false;
            _this.childSelector.classList.remove("while-dragging");
        });
        // Mouse Leave Function
        this.childSelector.addEventListener("mouseleave", function () {
            _this.isDown = false;
            _this.childSelector.classList.remove("while-dragging");
        });
        // Mouse Down Function
        this.childSelector.addEventListener("mousedown", function (e) {
            e.preventDefault();
            _this.isDown = true;
            _this.childSelector.classList.add("while-dragging");
            scrollX = e.pageX - _this.childSelector.offsetLeft;
            _this.scrollLeft = _this.childSelector.scrollLeft;
        });
        // Mouse Move Function
        this.childSelector.addEventListener("mousemove", function (e) {
            if (!_this.isDown)
                return;
            e.preventDefault();
            var element = e.pageX - _this.childSelector.offsetLeft;
            var scrolling = (element - scrollX) * ((_this.options.threshold) ? _this.options.threshold : 2);
            _this.childSelector.scrollLeft = _this.scrollLeft - scrolling;
        });
        this.childSelector.addEventListener("scroll", this.debounce(function (e) { _this.controlVisibilityDebounce(); }, 50));
    };
    DragSlider.prototype.controlVisibilityDebounce = function () {
        this.controlVisibility();
    };
    DragSlider.prototype.debounce = function (func, timer) {
        let timeout = null;
        return (...args) => {
          if (timeout) clearTimeout(timeout);
          timeout = setTimeout(() => {
            func(...args);
          }, timer);
        }
      }
    DragSlider.prototype.controlVisibility = function () {
        if (this.childSelector.scrollLeft === 0) {
            this.leftControlVisibility(false);
        }
        else {
            this.leftControlVisibility(true);
        }
        if (this.childSelector.scrollLeft ===
            this.childSelector.scrollWidth - this.childSelector.clientWidth) {
            this.rightControlVisibility(false);
        }
        else {
            this.rightControlVisibility(true);
        }
    };
    DragSlider.prototype.leftControlVisibility = function (visible) {
        this.leftOverlay.style.opacity = visible ? '1' : '0';
    };
    ;
    DragSlider.prototype.rightControlVisibility = function (visible) {
        this.rightOverlay.style.opacity = visible ? '1' : '0';
    };

    