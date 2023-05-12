Feature: SnackPurchase

A short summary of the feature

@snackpurchase
Scenario: Successful snack purchase
    Given I am an spectator
    And there are snacks available
    And I select one snack with id 1 and quantity 1
    When I confirm the purchase
    Then the purchase is completed
    