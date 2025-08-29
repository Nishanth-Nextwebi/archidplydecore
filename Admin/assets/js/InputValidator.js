class DecimalInputValidator {
    static init(selector) {
        $(document).on('input', selector, function () {
            DecimalInputValidator.validate($(this));
        });

        $(document).on('paste', selector, function () {
            let element = $(this);
            setTimeout(function () {
                DecimalInputValidator.validate(element);
            }, 0);
        });

        $(document).on('blur', selector, function () {
            DecimalInputValidator.validate($(this));
        });
    }

    static validate($element) {
        let value = $element.val();
        let validValue = DecimalInputValidator.getValidValue(value);
        if (value !== validValue) {
            $element.val(validValue);
        }
    }

    static getValidValue(value) {
        // Allow only numbers and one period, and limit to two decimal places
        let regex = /^(\d+(\.\d{0,2})?)?$/;

        if (regex.test(value)) {
            return value;
        } else {
            // Remove invalid characters
            return value.replace(/[^0-9.]/g, '')
                .replace(/(\..*)\./g, '$1')
                .replace(/(\.\d{2})\d+/g, '$1');
        }
    }
}


class TaxValidator {
    static init(selector) {
        $(document).on('input', selector, function () {
            TaxValidator.validate($(this));
        });

        $(document).on('paste', selector, function () {
            let element = $(this);
            setTimeout(function () {
                TaxValidator.validate(element);
            }, 0);
        });

        $(document).on('blur', selector, function () {
            TaxValidator.validate($(this));
        });
    }

    static validate($element) {
        let value = $element.val();
        let validValue = TaxValidator.getValidValue(value);
        if (value !== validValue) {
            $element.val(validValue);
        }
    }

    static getValidValue(value) {
        // Allow only numbers with up to 2 digits before the decimal point and 2 digits after
        let regex = /^(\d{0,2})(\.\d{0,2})?$/;
        if (regex.test(value)) {
            return value;
        } else {
            // Remove invalid characters
            value = value.replace(/[^0-9.]/g, ''); // Remove all non-numeric characters except .
            let parts = value.split('.');
            if (parts[0].length > 2) {
                parts[0] = parts[0].slice(0, 2); // Limit to 2 digits before decimal
            }
            if (parts.length > 1 && parts[1].length > 2) {
                parts[1] = parts[1].slice(0, 2); // Limit to 2 digits after decimal
            }
            return parts.join('.');
        }
    }
}


class OnlyNumbersValidator {
    static init(selector) {
        $(document).on('input', selector, function () {
            OnlyNumbersValidator.validate($(this));
        });

        $(document).on('paste', selector, function () {
            let element = $(this);
            setTimeout(function () {
                OnlyNumbersValidator.validate(element);
            }, 0);
        });

        $(document).on('blur', selector, function () {
            OnlyNumbersValidator.validate($(this));
        });
    }

    static validate($element) {
        let value = $element.val();
        let validValue = OnlyNumbersValidator.getValidValue(value);
        if (value !== validValue) {
            $element.val(validValue);
        }
    }

    static getValidValue(value) {
        // Allow only numbers
        let regex = /^\d*$/;
        if (regex.test(value)) {
            return value;
        } else {
            // Remove invalid characters
            value = value.replace(/[^0-9]/g, '');
            return value;
        }
    }
}


class OnlyAlphabetsValidator {
    static init(selector) {
        $(document).on('input', selector, function () {
            OnlyAlphabetsValidator.validate($(this));
        });

        $(document).on('paste', selector, function () {
            let element = $(this);
            setTimeout(function () {
                OnlyAlphabetsValidator.validate(element);
            }, 0);
        });

        $(document).on('blur', selector, function () {
            OnlyAlphabetsValidator.validate($(this));
        });
    }

    static validate($element) {
        let value = $element.val();
        let validValue = OnlyAlphabetsValidator.getValidValue(value);
        if (value !== validValue) {
            $element.val(validValue);
        }
    }

    static getValidValue(value) {
        // Allow only alphabets and spaces
        let regex = /^[a-zA-Z\s]*$/;
        if (regex.test(value)) {
            return value;
        } else {
            // Remove invalid characters
            value = value.replace(/[^a-zA-Z\s]/g, '');
            return value;
        }
    }
}
