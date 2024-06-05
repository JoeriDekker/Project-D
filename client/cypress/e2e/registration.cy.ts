describe('Able to visit', () => {
  it('is able to visit the registration page', () => {
    cy.visit('http://localhost:3000/register')
    cy.location('pathname').should('eq', '/register')
  })
})
describe('Unable to enter too small data', () => {
  beforeEach(() => {
    cy.visit('http://localhost:3000/register')
    cy.get('input[name=firstname]').as('firstnameInput')
    cy.get('input[name=lastname]').as('lastnameInput')
    cy.get('input[name=email]').as('emailInput')
    cy.get('input[name=password]').as('passwordInput')
  })
  it('is unable to enter too small firstname', () => {
    cy.get('@firstnameInput').type('s', { delay: 0})
    cy.get('#firstname-error').should('not.be.empty')
  })
  it('is unable to enter too small lastname', () => {
    cy.get('@lastnameInput').type('s', { delay: 0})
    cy.get('#lastname-error').should('not.be.empty')
  })
  it('is unable to enter too small email', () => {
    cy.get('@emailInput').type('s', { delay: 0})
    cy.get('#email-error').should('not.be.empty')
  })
  it('is unable to enter too small password', () => {
    cy.get('@passwordInput').type('s'.repeat(7), { delay: 0})
    cy.get('#password-error').should('not.be.empty')
  })
})

describe('Unable to enter too big data', () => {
  beforeEach(() => {
    cy.visit('http://localhost:3000/register')
    cy.get('input[name=firstname]').as('firstnameInput')
    cy.get('input[name=lastname]').as('lastnameInput')
    cy.get('input[name=email]').as('emailInput')
    cy.get('input[name=password]').as('passwordInput')
  })

  it('is unable to enter too big firstname', () => {
    cy.get('@firstnameInput').type('s'.repeat(51), { delay: 0 })
    cy.get('#firstname-error').should('not.be.empty')
  })

  it('is unable to enter too big lastname', () => {
    cy.get('@lastnameInput').type('s'.repeat(51), { delay: 0 })
    cy.get('#lastname-error').should('not.be.empty')
  })

  it('is unable to enter too big email', () => {
    cy.get('@emailInput').type('s'.repeat(51), { delay: 0 })
    cy.get('#firstname-email').should('not.be.empty')
  })

  it('is unable to enter too big password', () => {
    cy.get('@passwordInput').type('s'.repeat(251), { delay: 0 })
    cy.get('#firstname-password').should('not.be.empty')
  })
})

describe('Unable to enter invalid email', () => {
  beforeEach(() => {
    cy.visit('http://localhost:3000/register')
    cy.get('input[name=firstname]').as('firstnameInput')
    cy.get('input[name=lastname]').as('lastnameInput')
    cy.get('input[name=email]').as('emailInput')
    cy.get('input[name=password]').as('passwordInput')
  })
    
  it('is unable to enter only letters', () => {
    cy.get('@emailInput').type('sssssss')
    cy.get('#email-error').should('not.be.empty')
  })

  it('is unable to enter only numbers', () => {
    cy.get('@emailInput').type('123456')
    cy.get('#email-error').should('not.be.empty')
  })

  it('is unable to enter only symbols', () => {
    cy.get('@emailInput').type('!@#$%^&*')
    cy.get('#email-error').should('not.be.empty')
  })

  it('is unable to enter incomplete email', () => {
    cy.get('@emailInput').type('test@tes')
    cy.get('#email-error').should('not.be.empty')
  })
})
