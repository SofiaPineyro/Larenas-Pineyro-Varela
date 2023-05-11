Feature: SnackPurchase

A short summary of the feature

@snackpurchase
Scenario: Successful snack purchase
    Given I am an spectator
    And there are snacks available
    When the user double clicks the snack
    Then the snack subtotal is added to the tickets subtotal
    And the user completes the purchase with the snack added

Scenario: Successful purchase of some snacks
    Given I am an spectator
    And there are snacks available
    And there is one snack selected
    When the user selects another snack
    Then the snack subtotal is added to the total
    And the user completes the purchase with the snacks added

Scenario: Successful purchase without snacks
    Given I am an spectator
    And there are snacks available
    But the user did not select any snack
    When the user clicks Confirm
    Then the user completes the purchase without any snack